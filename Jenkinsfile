pipeline {
  agent any
  stages {
    stage('Clean workspace') {
      steps {
        cleanWs(skipWhenFailed: true)
      }
    }

    stage('Test') {
      steps {
        echo ${workspace}
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
