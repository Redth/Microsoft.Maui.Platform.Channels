plugins {
    id("com.android.library")
}

//configurations.implementation.canBeResolved = true

android {
    namespace = "com.microsoft.maui.platform.channels.sample"

    compileSdk = 33

    defaultConfig {
        minSdk = 21

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
    }

    buildTypes {
        getByName("release") {
            isMinifyEnabled = true
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
}





dependencies {

    //compileOnly(project(":dotnet_gradle_plugin"))
    implementation("androidx.appcompat:appcompat:1.6.1")
    implementation("com.google.android.material:material:1.10.0")
    implementation(project(":platformchannels"))
    testImplementation("junit:junit:4.13.2")
    // androidTestImplementation 'androidx.test.ext:junit:1.1.5'
    // androidTestImplementation 'androidx.test.espresso:espresso-core:3.5.1'
}