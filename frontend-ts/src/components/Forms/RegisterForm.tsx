import React from "react";
import MakeApiCall from "../../apiRequests/MainApiRequester";


type RegisterFormProps = {
    onLogin: (arg0: string) => void
}

type RegisterRequest = {
    login: string,
    password: string
}


type RegisterFormState = {
    login: string,
    password: string,
    passwordRepeat: string,
    errors: string,
}

export default class RegisterForm extends React.Component<RegisterFormProps, RegisterFormState>{
    constructor(props: RegisterFormProps) {
        super(props);

        this.handle_submit = this.handle_submit.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.validate =this.validate.bind(this);
    }
    state: RegisterFormState = {
        login: "",
        password: "",
        passwordRepeat: "",
        errors: ""
    }

    validate(): boolean{
        if (this.state.password !== this.state.passwordRepeat){
            this.setState({errors: "Passwords doesn't match"})
            return false;
        }

        return true;
    }

    handle_submit(event: any){
            if(this.validate()){
            MakeApiCall<RegisterRequest, string>("/Accounts/Register", "POST", false,
                {login: this.state.login, password: this.state.password})
                .then(result => {
                    if (result.isSuccess){
                        this.props.onLogin(result.body??"")
                    }
                    else{
                        this.setState({errors: result.errors})
                    }
                })
        }
        event.preventDefault();
    }


    handleChange(evt: React.ChangeEvent<HTMLInputElement>){
        const value = evt.target.value;
        this.setState({
            ...(this.state),
            [evt.target.name]: value
        });
    }

    render(){
        return(
            <form className={"login-form"} onSubmit={this.handle_submit}>
                <h3>Register</h3>
                <label htmlFor={"login"}>Login: </label>
                <input id={"login"} name={"login"} type={"text"} required={true} minLength={6}
                       value={this.state.login} onChange={this.handleChange}/>
                <label htmlFor={"password"}>Password: </label>
                <input id={"password"} name={"password"} type={"password"} minLength={6}
                       required={true} value={this.state.password} onChange={this.handleChange}/>
                <label htmlFor={"passwordRepeat"}>Repeat password: </label>
                <input id={"passwordRepeat"} name={"passwordRepeat"} type={"password"} minLength={6}
                       required={true} value={this.state.passwordRepeat} onChange={this.handleChange}/>
                <span id={"form-errors"}>{this.state.errors}</span>
                <input className={"primary-form-button"} type={"submit"} value={"Register"} />
            </form>
        )
    }

}