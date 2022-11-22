import React from "react";
import { Redirect, Route, Switch } from 'react-router-dom';
import ForgotPassword from "../../../pages/ForgotPassword/ForgotPassword";
import Home from "../../../pages/Home/Home";
import Login from '../../../pages/Login/Login';
import SignUp from '../../../pages/SignUp/SignUp';
import Users from '../../../pages/Users/Users';
import ValidateUser from "../../../pages/ValidateUser/ValidateUser";
import AuthContext from "../../../store/auth-context";
import Layout from "../Layout/Layout";
import { useContext } from 'react';

const Routes = () => {
    const authCtx = useContext(AuthContext)

    const unathorizedUrls = [
        <Route path="/login" key="/login">
            <Login />
        </Route>,
        <Route path="/sign-up" key="/sign-up">
            <SignUp />
        </Route>,
        <Route path="/validate-account/:user" key="/validate-account/:user">
            <ValidateUser />
        </Route>,
        <Route path="/forgot-password" exact key="/forgot-password">
            <ForgotPassword />
        </Route>,
        <Route path="/forgot-password/:token" key="/forgot-password/:token">
            <ForgotPassword />
        </Route>
    ];

    const authorizedUrls = [
        <Route path="/users" key="/users">
            <Layout>
                <Users />
            </Layout>
        </Route>,      
    ];


    return (
        <React.Fragment>
            <Switch>
                <Route path="/" exact>
                    <Layout>
                        <Home />
                    </Layout>
                </Route>
                {unathorizedUrls}
                {authCtx.isLoggedIn && authorizedUrls}
                <Route path="*">
                    <Layout>
                        <Redirect to={'/'} />
                    </Layout>
                </Route>
            </Switch>
        </React.Fragment>
    );
}

export default Routes;