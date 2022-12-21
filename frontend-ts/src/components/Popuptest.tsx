import React from "react";
import Popup from "reactjs-popup";
import LoginForm from "./LoginForm";

export default function PopUpComponent() {
    return (
        <Popup trigger={<button className="button"> Open Modal </button>} modal>
            <div className={"modal"}>
                <LoginForm />
            </div>
        </Popup>
    );
}
