pipeline {
  agent any
  stages {
    stage('Clean workspace') {
      steps {
        echo 'Clean'
      }
    }

    stage('Restore packages') {
      steps {
        sh 'ls -a'
        sh 'dotnetRestore DotnetCrossCutting.sln'
      }
    }

    stage('Deploy') {
      steps {
        echo 'Deploying....'
      }
    }

  }
  environment {
    ENV_TEST = '1'
  }
}