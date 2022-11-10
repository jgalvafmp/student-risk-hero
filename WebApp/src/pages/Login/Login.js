import Card from '../../components/core/Card/Card';
import Input from '../../components/core/Input/Input';
import Button from '../../components/core/Button/Button';
import './Login.scss';

const Login = props => {
    return (
        <div className="srhero__login--container">
            <Card>
                <form>
                    <h1>Login</h1>
                    <Input label="Usuario" type="text" plaholder="Digite su nombre de usuario" />
                    <Input label="Contraseña" type="password" plaholder="Digite su contraseña" />
                    <Button>Login</Button>
                </form>
            </Card>
        </div>
    );
}

export default Login;