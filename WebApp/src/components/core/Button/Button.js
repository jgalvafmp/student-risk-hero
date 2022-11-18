import './Button.scss';

const Button = props => {
    console.log(props.disabled)
    return (
        <button
            className="srhero__button"
            type={props.type ? props.type : 'button'}
            disabled={props.disabled}
            onClick={props.onClick}
        >
            {props.children}
        </button>
    );
}

export default Button;