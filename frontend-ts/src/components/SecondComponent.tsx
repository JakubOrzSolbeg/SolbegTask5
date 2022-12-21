import React from "react";
import {useParams} from "react-router-dom";

export default function SecondComponent(){
    let { userId } = useParams();
    console.log(userId);
    console.log(typeof userId)
    return(
        <p> Secondary component with id {userId} </p>
    )
}