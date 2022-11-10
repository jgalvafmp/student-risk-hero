const AuthService = {
    login: (username, password) => {
        console.log('Login!', username, password)
    },
    signUp: (userData) => {
        console.log('signUp!', userData)
    }
}

export default AuthService;