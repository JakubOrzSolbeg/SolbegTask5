import React, {createRef, useRef, useState} from 'react';
import Popup from "reactjs-popup";
import LoginForm from "./LoginForm";
import {PopupActions} from "reactjs-popup/dist/types";

export default function ControlledPopup() {
    const ref = useRef(null);

    const closeTooltip = () => {
        let ref2 = createRef();
        // @ts-ignore
        ref.current.close();
    }

    return(
        <Popup ref={ref} trigger={<button> Open this shit </button>} modal>
            <button onClick={closeTooltip}> Close popup </button>
        </Popup>
    );

}