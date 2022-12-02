import '../SignUp/SignUp.scss';
import Card from '../../../components/core/Card/Card';
import logo from '../../../logo.svg';
import Button from '../../../components/core/Button/Button';
import { useHistory, useParams } from 'react-router-dom';
import React, { useEffect, useState } from 'react';
import AuthService from '../../../services/AuthService';
import Spinner from '../../../components/core/Layout/Spinner/Spinner';


const ValidateUser = () => {
    const [loading, setLoading] = useState(false);

    const { user } = useParams();

    const [state, setState] = useState('Your account has been validated')

    useEffect(() => {
        setLoading(true)
        AuthService.activateUser(user).then(data => {
            try{;
                return data.json()
            } catch {
                setState('There was an error validating your account');
                setLoading(false)
            }
        }).then(res => {
            setLoading(false);
            if(res.toString().indexOf("was not found") > -1) {
                setState('There was an error validating your account')
            }
        });
        // eslint-disable-next-line
    }, [])

    const history = useHistory();

    const returnToLogin = () => {
        history.push('/login')
    }

    return (
        <React.Fragment>
            {loading && <Spinner />}
            <div className="srhero__sign-up--container">
                <div className="card--container col-xs-12 col-md-8 col-lg-9">
                    <Card>
                        <form>
                            <div className='logo'>
                                <img src={logo} alt="logo" width={'200px'} />
                            </div>
                            <div className='content'>
                                <div className='col-xs-12'>
                                    {state}
                                </div>
                                <div className='button-section col-xs-12'>
                                    <div className='row'>
                                        <div className='col-xs-6 col-xs-offset-3'>
                                            <Button type='click' onClick={returnToLogin}>Return to login</Button>
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

export default ValidateUser;