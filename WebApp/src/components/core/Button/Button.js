import './Button.scss';

const Button = props => {
    return (
        <button
            className="srhero__button"
            type={props.type ? props.type : 'button'}
        >
            {props.children}
        </button>
    );
}

export default Button;