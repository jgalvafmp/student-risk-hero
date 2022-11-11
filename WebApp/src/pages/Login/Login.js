import Card from '../../components/core/Card/Card';
import Input from '../../components/core/Input/Input';
import Button from '../../components/core/Button/Button';
import './Login.scss';
import { useState } from 'react';
import AuthService from '../../services/AuthService';
import { useHistory, Link } from 'react-router-dom';
import logo from '../../logo.svg';

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
           
            <div className="col-xs-12 col-md-6 col-lg-4">
                <Card>
                    <form onSubmit={onLogin}>
                        <div className='logo'>
                            <img src={logo} alt="logo" width={'200px'} />
                        </div>
                        <div className='content'>
                            <h1>Login</h1>
                            <Input 
                                label="Usuario" 
                                value={state.Username} 
                                type="text" 
                                plaholder="Digite su nombre de usuario"
                                error={state.errorUsername}
                                onChange={onInputUsernameChange} />
                            <Input 
                                label="Contraseña" 
                                type="password" 
                                plaholder="Digite su contraseña"
                                error={state.errorPassword}
                                value={state.Password}
                                onChange={onInputPasswordChange} />
                            {state.error && <span className='label-error'>{state.error}</span>}
                            <Button type="submit">Login</Button>
                            <Link to='sign-up '>Registrarme</Link>
                        </div>
                    </form>
                </Card>
            </div>
        </div>
    );
}

export default Login;