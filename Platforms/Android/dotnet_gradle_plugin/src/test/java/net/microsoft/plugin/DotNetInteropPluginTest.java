package net.microsoft.plugin;

import org.gradle.testfixtures.ProjectBuilder;
import org.gradle.api.Project;
import org.junit.Test;
import static org.junit.Assert.assertNotNull;


public class DotNetInteropPluginTest {
    @Test
    public void pluginRegistersATask() {
        // Create a test project and apply the plugin
        Project project = ProjectBuilder.builder().build();
        project.getPlugins().apply("net.microsoft.plugin.dotnetinterop");

        // Verify the result
        assertNotNull(project.getTasks().findByName("resolveArtifacts"));
    }
}
