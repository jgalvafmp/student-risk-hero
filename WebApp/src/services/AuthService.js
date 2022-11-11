import HttpService from './HttpService';

const AuthService = {
    login: (username, password) => {
        return new Promise((resolve, reject) => {
            HttpService.post(
                //'http://www.student-risk-hero.somee.com/api/Auth/login',
                'https://localhost:7006/api/Auth/login',
                {
                    "username": username,
                    "password": password
                }
            ).then(response => {
                resolve(response)
            }).catch(error => {
                reject(error.json());
            });
        });
    },
    signUp: (userData) => {
        console.log('signUp!', userData)
    }
}

export default AuthService;