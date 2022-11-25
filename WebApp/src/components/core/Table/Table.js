import React from "react";
import Button from "../Button/Button";
import "./Table.scss";

const Table = (props) => {
    const thead = (
        <tr>
           {props.header.map(header => {
                const classes = header === '' ? 'option-th' : '';
                return <th className={classes}>{header}</th>
            })}
        </tr>
    );
    
    const tbody = (
        <React.Fragment>
           {props.data.map(body => {
                return (
                    <tr key={body["id"]}>
                        {props.rows.map(key => {
                            const row = key !== "options" ? <td>{body[key]}</td> 
                                                          :  <td className="options">
                                                                {props.editHandler && <Button onClick={() => props.editHandler(body["id"])}>Edit</Button>}
                                                                {props.deleteHandler && <Button onClick={() => props.deleteHandler(body["id"])}>Delete</Button>}
                                                            </td>;
                            return row;
                        })}
                    </tr>
                );
            })}
        </React.Fragment>
    );

    return (
        <table className="table table-hover">
            <thead>
                {thead}
            </thead>
            <tbody>
                {tbody}
            </tbody>
        </table>
    );
}

export default Table;