import HttpService from './HttpService';
import ENV from '../utils/env';

const AuthService = {
    login: (username, password) => {
        return new Promise((resolve, reject) => {
            HttpService.post(
                `${ENV.apiURL}/Auth/login`,
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
        return new Promise((resolve, reject) => {
            HttpService.post(
                `${ENV.apiURL}/Auth/sign-in`,
                userData
            ).then(response => {
                resolve(response)
            }).catch(error => {
                reject(error.json());
            });
        });
    },
    forgotPassword: (password, token) => {
        return new Promise((resolve, reject) => {
            HttpService.put(
                `${ENV.apiURL}/Auth/forgot-password/user/${token}`,
                { password: password }
            ).then(response => {
                resolve(response)
            }).catch(error => {
                reject(error.json());
            });
        });
    },
    forgetPassword: (user) => {
        return new Promise((resolve, reject) => {
            HttpService.post(
                `${ENV.apiURL}/Auth/forgot-password/user/${user}`
            ).then(response => {
                resolve(response)
            }).catch(error => {
                reject(error.json());
            });
        });
    },
    activateUser: (user) => {
        return new Promise((resolve, reject) => {
            HttpService.post(
                `${ENV.apiURL}/Auth/validate/user/${user}`,
            ).then(response => {
                resolve(response)
            }).catch(error => {
                reject(error.json());
            });
        });
    }
}

export default AuthService;