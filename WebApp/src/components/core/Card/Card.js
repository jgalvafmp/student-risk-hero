import './Card.scss';

export const Card = props => {
    return (
        <div className="srhero__card">
            {props.children}
        </div>
    );
}

export default Card;