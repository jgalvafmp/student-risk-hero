import '../SignUp/SignUp.scss';
import Card from '../../../components/core/Card/Card';
import logo from '../../../logo.svg';
import Button from '../../../components/core/Button/Button';
import { useHistory, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import AuthService from '../../../services/AuthService';


const ValidateUser = () => {

    const { user } = useParams();

    const [state, setState] = useState('Your account has been validated')

    useEffect(() => {
        AuthService.activateUser(user).then(data => {
            try{
                return data.json()
            } catch {
                setState('There was an error validating your account')
            }
        }).then(res => {
            if(res.toString().indexOf("was not found") > -1) {
                setState('There was an error validating your account')
            }
        });
    })

    const history = useHistory();

    const returnToLogin = () => {
        history.push('/login')
    }

    return (
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
    );
}

export default ValidateUser;