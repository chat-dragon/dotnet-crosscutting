pipeline {
  agent any
  stages {
    stage('Clean workspace') {
      agent any
      steps {
        sh 'cleanWs()'
      }
    }

    stage('Test') {
      steps {
        echo 'Testing..'
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