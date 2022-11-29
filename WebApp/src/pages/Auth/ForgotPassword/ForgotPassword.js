import '../SignUp/SignUp.scss';
import Card from '../../../components/core/Card/Card';
import Input from '../../../components/core/Input/Input';
import Button from '../../../components/core/Button/Button';
import AuthService from '../../../services/AuthService';
import logo from '../../../logo.svg';
import readers from '../../../assets/images/thinking.png';
import { useState } from 'react';
import { useHistory, useParams } from 'react-router-dom';
import Spinner from '../../../components/core/Layout/Spinner/Spinner';
import React from 'react';
import { ErrorAlert, SuccessAlert } from '../../../services/AlertService';

const ForgotPassword = () => {
    const history = useHistory();

    const [loading, setLoading] = useState(false);

    const { token } = useParams();

    const [state, setState] = useState(
        {
            Username: '',
            Password: '',
            confirmPassword: '',
            errors: {
                Password: undefined,
                confirmPassword: undefined,
                Username: undefined,
            }
        }
    );

    const onInputChange = (event, field) => {
        setState((prevState) => {
            const newState = {...prevState};
            newState[field] = event.target.value;
            if (field === "Password" && newState.Password.length < 4) {
                newState.errors.Password = "Password length should be 4 characteres or more"
            } else {
                newState.errors.Password = undefined;
            }
            if (field === "confirmPassword" && newState.confirmPassword !== newState.Password) {
                newState.errors.confirmPassword = "Confirm password should match password."
            } else {
                newState.errors.confirmPassword = undefined;
            }
            return newState;
        });
    }

    const addValidation = (data, field) => {
        data.json().then(error => {
            if(error.toString().toLowercase().indexOf(field) > -1){
                setState((prevState) => {
                    const newState = {...prevState};
                    newState.errors[field] = error;
                    return newState;
                });
            }
        })
    }

    const returnToLogin = () => {
        history.push('/login')
    }

    const onSubmit = (e) => {
        e.preventDefault();
        setLoading(true);
        const data = {...state};

        if (token) {
            AuthService.forgotPassword(data.Password, token)
            .then(response => {
                setLoading(false);
                if(response.status === 400) {
                    addValidation(response);
                } else {
                    SuccessAlert("Operation successful", "Password have been changed");
                }
            });
        } else {
            AuthService.forgetPassword(data.Username)
            .then(async response => {
                const res = await response.json();
                if (res.indexOf("not found") > -1) {
                    ErrorAlert("Error", "Password could not be changed");
                    setState(prevState => {
                        const newState = { ...prevState };
                        newState.errors.Username = res;
                        return newState;
                    });
                }
            });
        }
    }

    let form = (
        <div className='col-xs-9'>
            <div className='row'>
                <div className='col-xs-12'>
                    <h2>User information</h2>
                </div>
                <div className='col-xs-12 col-md-12 col-lg-12'>
                    <Input 
                        label="Username or email" 
                        value={state.Username} 
                        type="Username" 
                        placeholder="Type your Username or email"
                        error={state.errors.Username}
                        onChange={(e) => {onInputChange(e, 'Username')}} />
                </div>
            </div>
        </div>
    );

    if (token) {
        form = (
            <div className='col-xs-9'>
                <div className='row'>
                    <div className='col-xs-12'>
                        <h2>Password information</h2>
                    </div>
                    <div className='col-xs-12 col-md-6 col-lg-6'>
                        <Input 
                            label="Password" 
                            value={state.Password} 
                            type="password" 
                            placeholder="Type your password"
                            error={state.errors.Password}
                            onChange={(e) => {onInputChange(e, 'Password')}} />
                    </div>
                    <div className='col-xs-12 col-md-6 col-lg-6'>
                        <Input 
                            label="Confirm Password" 
                            value={state.confirmPassword} 
                            type="password" 
                            placeholder="Confirm your password"
                            error={state.errors.confirmPassword}
                            onChange={(e) => {onInputChange(e, 'confirmPassword')}} />
                    </div>
                </div>
            </div>
        );
    }

    return (
        <React.Fragment>
            {loading && <Spinner />}
            <div className="srhero__sign-up--container">
                <div className="card--container col-xs-12 col-md-8 col-lg-9">
                    <Card>
                        <form onSubmit={onSubmit}>
                            <div className='logo'>
                                <img src={logo} alt="logo" width={'200px'} />
                            </div>
                            <div className='content'>
                                <h1>Login back in form</h1>
                                <div className='row'>
                                    <div className='col-xs-3'>
                                        <img src={readers} alt="readers" width={'70%'} />
                                    </div>
                                    {form}
                                    <div className='button-section col-xs-12'>
                                        <div className='row'>
                                            <div className='col-xs-6 col-xs-offset-3'>
                                                <Button 
                                                    type='submit'
                                                    disabled={!(!(state.Password !== state.confirmPassword && state.Password > 3) || !token)}>
                                                        {token ? 'Change password' : 'Request new password'}
                                                </Button>
                                                <br />
                                                <Button type='click' onClick={returnToLogin}>Return to login</Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </Card>
                </div>
            </div>
        </React.Fragment>
    );
}

export default ForgotPassword; 