import React from "react";
import { Route, Switch } from 'react-router-dom';
import ForgotPassword from "../../../pages/Auth/ForgotPassword/ForgotPassword";
import Home from "../../../pages/Home/Home";
import Login from '../../../pages/Auth/Login/Login';
import SignUp from '../../../pages/Auth/SignUp/SignUp';
import Users from '../../../pages/Auth/Users/Users';
import ValidateUser from "../../../pages/Auth/ValidateUser/ValidateUser";
import Layout from "../Layout/Layout";
import CoursePage from "../../../pages/School/Courses/Course";

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
                </Route>
                <Route path="/courses">
                    <Layout>
                        <CoursePage />
                    </Layout>
                </Route>
                <Route path="*">
                    <Layout>
                        <Home />
                    </Layout>
                </Route>
            </Switch>
        </React.Fragment>
    );
}

export default Routes;