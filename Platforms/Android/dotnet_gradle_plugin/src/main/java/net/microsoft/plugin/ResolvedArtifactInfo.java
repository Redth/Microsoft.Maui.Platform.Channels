package net.microsoft.plugin;

import org.gradle.api.artifacts.ResolvedArtifact;
import org.gradle.api.provider.Property;
import org.gradle.api.tasks.Input;

import java.io.File;

public abstract class ResolvedArtifactInfo {


    @Input
    public abstract Property<String> getArtifactId();

    @Input
    public abstract Property<String> getGroupId();

    @Input
    public abstract Property<String> getVersion();

    @Input
    public abstract Property<File> getFile();

}
