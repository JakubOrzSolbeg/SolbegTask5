import React from "react";
import {apiBaseString} from "./requestsConfig";
import ApiResult from "../models/ApiResultBase";

export default function Login(login: string, password: string): Promise<ApiResult<string>> {
    return fetch(apiBaseString+"/Accounts/Login", {
        method: "POST",
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            "login": login,
            "password": password
        })
    })
        .then(res => res.json())
        .then(res => {
            return res as ApiResult<string>
        })
}