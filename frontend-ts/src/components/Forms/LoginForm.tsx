import React from "react";
import Login from "../../apiRequests/login";

type LoginFormProps = {
    onLogin: (arg0: string) => void
}


type LoginFormState = {
    login: string,
    password: string,
    errors: string,
    canBeSubmitted: boolean
}

export default class LoginForm extends React.Component<LoginFormProps, LoginFormState>{
    constructor(props: LoginFormProps) {
        super(props);

        this.handle_submit = this.handle_submit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }
    state: LoginFormState = {
        login: "",
        password: "",
        errors: "",
        canBeSubmitted: false
    }

    handle_submit(event: any){
        event.preventDefault();
        Login(this.state.login, this.state.password).then(result => {
            if(!result.isSuccess){
                this.setState({errors: result.errors})
            }
            else{
                console.log(result.body);
                this.props.onLogin(result.body??"");
            }
        })
    }

    componentDidUpdate(prevProps:any, prevState:LoginFormState) {
        if (this.state.login.length > 5 && this.state.password.length > 5){
            if (!this.state.canBeSubmitted){
                this.setState({canBeSubmitted: true})
            }
        }
        else{
            if (this.state.canBeSubmitted){
                this.setState({canBeSubmitted: false})
            }
        }
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
                <h3>Log in to continue</h3>
                <label htmlFor={"login"}>Login: </label>
                <input id={"login"} name={"login"} type={"text"} required={true}
                       value={this.state.login} onChange={this.handleChange}/>
                <label> Password: </label>
                <input id={"password"} name={"password"} type={"password"}
                       required={true} value={this.state.password} onChange={this.handleChange}/>
                <span id={"form-errors"}>{this.state.errors}</span>
                <input className={"primary-form-button"} type={"submit"} value={"Log in"} disabled={!this.state.canBeSubmitted}/>
            </form>
        )
    }

}