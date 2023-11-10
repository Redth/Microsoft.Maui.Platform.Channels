package net.microsoft.plugin;

import org.gradle.api.Plugin;
import org.gradle.api.Project;
import org.gradle.api.Transformer;
import org.gradle.api.artifacts.result.ResolvedArtifactResult;
import org.gradle.api.model.ObjectFactory;
import org.gradle.internal.component.external.model.DefaultModuleComponentIdentifier;

import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

public class DotNetInteropPlugin implements Plugin<Project> {
    public void apply(Project project) {

        // Register our task
        project.getTasks().register("dotnetInteropResolveArtifacts", DotNetInteropResolveArtifactsTask.class, task -> {
            task.getResolvedArtifactsOutput().set(project.getLayout().getBuildDirectory().file("dotnetInteropResolvedArtifacts.txt"));

            // Get the right config
            var configs = project.getConfigurations();
            var runtimeClasspath = configs.getByName("debugRuntimeClasspath");

            task.getResolvedArtifacts().set(runtimeClasspath.getIncoming().getArtifacts().getResolvedArtifacts().map(new ResolvedArtifactInfoTransformer(project.getObjects())));
            task.resolveArtifacts();

        });
    }

    // This class transforms the ResolvedArtifactResult type into our Info type which is gradle friendly
    // to be used as @Input in our task
    static class ResolvedArtifactInfoTransformer implements Transformer<List<ResolvedArtifactInfo>, Collection<ResolvedArtifactResult>> {
        public ResolvedArtifactInfoTransformer(ObjectFactory objects)
        {
            this.objects = objects;
        }

        ObjectFactory objects;

        @Override
        public List<ResolvedArtifactInfo> transform(Collection<ResolvedArtifactResult> artifacts) {
            return artifacts.stream().map(a -> {
                // Since the info class is abstract, this is how we instantiate it with Gradle
                ResolvedArtifactInfo info = objects.newInstance(ResolvedArtifactInfo.class);

                // Set the file on the info
                info.getFile().set(a.getFile());

                // Check if we can cast to the type that gives us maven artifact info
                if (a.getId().getComponentIdentifier() instanceof DefaultModuleComponentIdentifier)
                {
                    var moduleId = ((DefaultModuleComponentIdentifier)a.getId().getComponentIdentifier());

                    // Set the maven group, artifact, and version
                    info.getGroupId().set(moduleId.getGroup());
                    info.getArtifactId().set(moduleId.getModule());
                    info.getVersion().set(moduleId.getVersion());
                }

                return info;
            }).collect(Collectors.toList());
        }
    }
}
