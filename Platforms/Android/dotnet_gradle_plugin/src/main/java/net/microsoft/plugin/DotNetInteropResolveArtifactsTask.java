package net.microsoft.plugin;

import org.gradle.api.DefaultTask;
import org.gradle.api.Transformer;
import org.gradle.api.artifacts.ResolvedArtifact;
import org.gradle.api.artifacts.result.ResolvedArtifactResult;
import org.gradle.api.file.RegularFileProperty;
import org.gradle.api.model.ObjectFactory;
import org.gradle.api.provider.ListProperty;
import org.gradle.api.provider.Property;
import org.gradle.api.provider.Provider;
import org.gradle.api.provider.SetProperty;
import org.gradle.api.tasks.Input;
import org.gradle.api.tasks.InputFiles;
import org.gradle.api.tasks.Nested;
import org.gradle.api.tasks.Optional;
import org.gradle.api.tasks.OutputFile;
import org.gradle.api.tasks.TaskAction;
import org.gradle.api.tasks.options.Option;
import org.gradle.internal.component.external.model.DefaultModuleComponentIdentifier;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardOpenOption;
import java.util.Collection;
import java.util.List;

public abstract class DotNetInteropResolveArtifactsTask extends DefaultTask {

    @OutputFile
    public abstract Property<File> getResolvedArtifactsOutput();

    @Option(option = "outputfile", description = "Sets the output file to print the dependency information to")
    @Input
    @Optional
    public abstract Property<String> getOptionalOutputFilePath();

    @Input
    public abstract ListProperty<ResolvedArtifactInfo> getResolvedArtifacts();

    File outputFile;

    void Append(String line)
    {
        try {
            Files.writeString(outputFile.toPath(), line + "\r\n", StandardOpenOption.APPEND);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    @TaskAction
    public void resolveArtifacts() {
        var resolvedArtifacts = this.getResolvedArtifacts().get();

        try {
            outputFile = getResolvedArtifactsOutput().get();
            Files.deleteIfExists(outputFile.toPath());
            if (!outputFile.exists()) {
                Files.createFile(outputFile.toPath());
            }
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        System.out.println("OUTPUT FILE: " + outputFile.getAbsolutePath());


        resolvedArtifacts.forEach(resolvedArtifact -> {
            var file = resolvedArtifact.getFile().get().getAbsolutePath();

            var groupId = resolvedArtifact.getGroupId().get();
            var artifactId = resolvedArtifact.getArtifactId().get();
            var version = resolvedArtifact.getVersion().get();

            if (groupId != null && artifactId != null && version != null &&  !groupId.isEmpty() && !artifactId.isEmpty() && !version.isEmpty())
            {
                Append("Maven," + groupId + "," + artifactId + "," + version + "," + file);
                System.out.println("MAVEN: " + groupId + "." + artifactId + "." + version + " -> " + file);
            }
            else if (file != null && !file.isEmpty())
            {
                Append("File," + file + ",,,");
                System.out.println("FILE: " + file);
            }
        });
    }

}
