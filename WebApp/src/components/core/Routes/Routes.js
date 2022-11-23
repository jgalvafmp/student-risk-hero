import React from "react";
import { Redirect, Route, Switch } from 'react-router-dom';
import ForgotPassword from "../../../pages/Auth/ForgotPassword/ForgotPassword";
import Home from "../../../pages/Home/Home";
import Login from '../../../pages/Auth/Login/Login';
import SignUp from '../../../pages/Auth/SignUp/SignUp';
import Users from '../../../pages/Auth/Users/Users';
import ValidateUser from "../../../pages/Auth/ValidateUser/ValidateUser";
import AuthContext from "../../../store/auth-context";
import Layout from "../Layout/Layout";
import { useContext } from 'react';
import CoursePage from '../../../pages/School/Courses/Course';

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
        <Route path="/courses" key="/courses">
            <Layout>
                <CoursePage />
            </Layout>
        </Route>
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