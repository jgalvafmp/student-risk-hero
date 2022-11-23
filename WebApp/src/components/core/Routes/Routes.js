import React from "react";
import { Route, Switch } from 'react-router-dom';
import ForgotPassword from "../../../pages/Auth/ForgotPassword/ForgotPassword";
import { Redirect, Route, Switch } from 'react-router-dom';
import ForgotPassword from "../../../pages/ForgotPassword/ForgotPassword";
import Home from "../../../pages/Home/Home";
import Login from '../../../pages/Auth/Login/Login';
import SignUp from '../../../pages/Auth/SignUp/SignUp';
import Users from '../../../pages/Auth/Users/Users';
import ValidateUser from "../../../pages/Auth/ValidateUser/ValidateUser";
import Login from '../../../pages/Login/Login';
import SignUp from '../../../pages/SignUp/SignUp';
import Users from '../../../pages/Users/Users';
import ValidateUser from "../../../pages/ValidateUser/ValidateUser";
import AuthContext from "../../../store/auth-context";
import Layout from "../Layout/Layout";
import CoursePage from "../../../pages/School/Courses/Course";
import { useContext } from 'react';

const Routes = props => {
    return (
        <React.Fragment>
            <Switch>
                <Route path="/login">
                <Login />
                </Route>
                <Route path="/sign-up">
                    <SignUp />
                </Route>
                <Route path="/validate-account/:user">
                    <ValidateUser />
                </Route>
                <Route path="/forgot-password" exact>
                    <ForgotPassword />
                </Route>
                <Route path="/forgot-password/:token">
                    <ForgotPassword />
                </Route>
                <Route path="/users">
                    <Layout>
                        <Users />
                    </Layout>
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
                <Route path="/courses">
                    <Layout>
                        <CoursePage />
                    </Layout>
                </Route>
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