import React from "react";
import { Route } from 'react-router-dom';
import Login from '../../../pages/Login/Login';
import SignUp from '../../../pages/SignUp/SignUp';
import Users from '../../../pages/Users/Users';
import Layout from "../Layout/Layout";

const Routes = props => {
    return (
        <React.Fragment>
            <Route path="/login">
                <Login />
            </Route>
            <Route path="/sign-up">
                <SignUp />
            </Route>
            <Route path="/users">
                <Layout>
                    <Users />
                </Layout>
            </Route>
        </React.Fragment>
    );
}

export default Routes;