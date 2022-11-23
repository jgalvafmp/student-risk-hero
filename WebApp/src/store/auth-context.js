import React, { useState } from 'react';

const AuthContext = React.createContext({
    isLoggedIn: false,
    token: '',
    login: (token) => {},
    logout: () => {}
});

export const AuthContextProvider = (props) => {
    const initialToken = localStorage.getItem('token');
    const [token, setToken] = useState(initialToken);

    const logoutHandler = () => {
        localStorage.removeItem('token');
        setToken(null);
    }

    const loginHandler = (token) => {
        localStorage.setItem('token', token);
        setToken(token);
    }

    const contextValue = {
        token,
        isLoggedIn: !!token,
        login: loginHandler,
        logout: logoutHandler
    };

    return (
        <AuthContext.Provider value={contextValue}>
            {props.children}
        </AuthContext.Provider>
    );
}

export default AuthContext;