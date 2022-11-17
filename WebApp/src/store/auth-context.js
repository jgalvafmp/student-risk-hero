import React, { useState } from 'react';

const AuthContext = React.createContext({
    isLoggedIn: false,
    logout: () => {},
    login: () => {}
});

const AuthContextProvider = (props) => {

    const [state, setState] = useState({
        isLoggedIn: false,
        username: '',
        role: '',
        token: ''
    });

    const logoutHandler = () => {
        localStorage.removeItem('token');
        setState(false);
    }

    const loginHandler = (token) => {
        localStorage.setItem('token', token);
        setState(() => {
            return {
                isLoggedIn: false,
                username: '',
                role: '',
                token: ''
            }
        });
    }

    return (
        <AuthContext.Provider value={
            {
                isLoggedIn: state.isLoggedIn,
                login: loginHandler,
                logout: logoutHandler
            }
        }>
            {props.children}
        </AuthContext.Provider>
    );
}

export default AuthContextProvider;