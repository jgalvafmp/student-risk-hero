import Card from '../../../components/core/Card/Card';
import Input from '../../../components/core/Input/Input';
import Button from '../../../components/core/Button/Button';
import './Login.scss';
import { useState } from 'react';
import AuthService from '../../../services/AuthService';
import { useHistory, Link } from 'react-router-dom';
import logo from '../../../logo.svg';
import login from '../../../assets/images/login.jpg';

const Login = () => {
    const history = useHistory();
    const [state, setState] = useState({
        Username: '',
        Password: '',
        errorPassword: undefined,
        errorUsername: undefined,
        error: undefined
    });

    const onInputUsernameChange = (event) => {
        setState((prevState) => {
            return {
                ...prevState,
                Username: event.target.value
            };
        });
    }

    const onInputPasswordChange = (event) => {
        setState((prevState) => {
            return {
                ...prevState,
                Password: event.target.value
            };
        });
    }

    const onLogin = (e) => {
        e.preventDefault();
        AuthService.login(state.Username, state.Password)
        .then(data => {
            switch(data.status) {
                case 200:
                    data.json().then((token) => {
                        localStorage.setItem('token', token);
                        history.push('/')
                    })
                  break;
                case 400:
                case 401:
                  addValidation(data)
                  break;
                default:
                  alert(data)
                  break;
            }
        }).catch(data => {
            console.log(data)
        });
    }

    const addValidation = (data) => {
        data.json().then(error => {
            console.log(error)
            if(error.toString().indexOf('Username') > -1){
                setState((prevState) => {
                    return {
                        ...prevState,
                        errorUsername: error
                    };
                });
            }
            if(error.toString().indexOf('Password') > -1){
                setState((prevState) => {
                    return {
                        ...prevState,
                        errorPassword: error
                    };
                });
            }
            if(error.toString().indexOf('credentials') > -1){
                setState((prevState) => {
                    return {
                        ...prevState,
                        error: error
                    };
                });
            }
        })
    }

    return (
        <div className="srhero__login--container">
            <div className="col-xs-12 col-md-8 col-lg-8">
                <Card>
                    <form onSubmit={onLogin}>
                        <div className='logo'>
                            <img src={logo} alt="logo" width={'200px'} />
                        </div>
                        <h1>Login</h1>    
                        <div className='row'>
                            <div className='col-xs-3'>
                                <img src={login} className='login' alt="login" />
                            </div>
                            <div className='col-xs-9'>
                                <div className='content'>                        
                                    <Input 
                                        label="Username" 
                                        value={state.Username} 
                                        type="text" 
                                        placeholder="Type your user's name"
                                        error={state.errorUsername}
                                        onChange={onInputUsernameChange} />
                                    <Input 
                                        label="Password" 
                                        type="password" 
                                        placeholder="Type your user's password"
                                        error={state.errorPassword}
                                        value={state.Password}
                                        onChange={onInputPasswordChange} />                                
                                    {state.error && <span className='label-error'>{state.error}</span>}
                                    <Button type="submit">Login</Button>
                                    <Link to='/sign-up'>Registrarme</Link>
                                    <Link to='/forgot-password'>Olvide mi contraseña</Link>
                                </div>
                            </div>
                        </div>
                    </form>
                </Card>
            </div>
        </div>
    );
}

export default Login;