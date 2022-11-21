import React, { useState } from "react";
import Button from "../../../components/core/Button/Button";
import Modal from "../../../components/core/Modal/Modal";

const CoursePage = () => {
    const [openForm, setOpenForm] = useState(false);

    const submitHandler = () => {
        console.log('submit')
    }

    const cancelHandler = () => {
        setOpenForm(false)
    }

    return (
        <React.Fragment>
            {openForm && <Modal 
                title={"Course form"}
                onSubmit={submitHandler}
                onCancel={cancelHandler}
            >
                <h1>Hello</h1>
            </Modal>}
            <div className="row">
                <div className="col-xs-6">
                    <h1>Course page</h1>
                </div>
                <div className="col-xs-6 align-end">
                    <div style={{width: "200px" }}>
                        <Button onClick={() => { setOpenForm(true) }}>
                            New course
                        </Button>
                    </div>
                </div>
                <div className="col-xs-12">
                    <table className="">
                    </table>
                </div>
            </div> 
        </React.Fragment>
    );
}

export default CoursePage;