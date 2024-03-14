pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'Stage 1: Building the code using Maven...'
            }
        }
        
        stage('Unit and Integration Tests') {
            steps {
                echo 'Stage 2: Running unit tests...'
                echo 'Stage 2: Running integration tests...'
            }
        }
        
        stage('Code Analysis') {
            steps {
                echo 'Stage 3: Performing code analysis using SonarQube...'
            }
        }
        
        stage('Security Scan') {
            steps {
                echo 'Stage 4: Performing security scan using OWASP ZAP...'
            }
        }
        
        stage('Deploy to Staging') {
            steps {
                echo 'Stage 5: Deploying to staging on AWS EC2 instance...'
            }
        }
        
        stage('Integration Tests on Staging') {
            steps {
                echo 'Stage 6: Running integration tests on staging environment...'
            }
        }
        
        stage('Deploy to Production') {
            steps {
                echo 'Stage 7: Deploying to production on AWS EC2 instance...'
            }
        }
    }
    
    post {
        success {
            echo 'Pipeline executed successfully.'
            emailext body: 'Pipeline executed successfully.',
                     subject: 'Pipeline Status: Success',
                     to: 'your_email@example.com'
        }
        failure {
            echo 'Pipeline execution failed.'
            emailext body: 'Pipeline execution failed.',
                     subject: 'Pipeline Status: Failure',
                     to: 'your_email@example.com'
        }
    }
}
