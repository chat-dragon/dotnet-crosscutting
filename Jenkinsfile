pipeline {
  agent any
  stages {
    stage('Clean workspace') {
      steps {
        echo 'Clean'
      }
    }

    stage('Test') {
      steps {
        echo 'workspace: '
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