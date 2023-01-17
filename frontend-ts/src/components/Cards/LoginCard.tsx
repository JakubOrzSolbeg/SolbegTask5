import React from "react";
import LoginForm from "../Forms/LoginForm";
import RegisterForm from "../Forms/RegisterForm";


type LoginCardProps = {
    onLogin: (arg0: string) => void
}

type LoginCardState = {
    isLogging: boolean
}


export class LoginCard extends React.Component<LoginCardProps, LoginCardState>{
    constructor(props: LoginCardProps) {
        super(props);
        this.toggleRegisterForm = this.toggleRegisterForm.bind(this);
    }

    state: LoginCardState = {
        isLogging: true
    }

    toggleRegisterForm(){
        this.setState({isLogging: false})
    }


    render() {
        if (this.state.isLogging){
            return(
                <div className={"login-card"}>
                    <LoginForm onLogin={this.props.onLogin} />
                    <button
                        className={"secondery-form-button"}
                        value={"Don't have an account?"}
                        onClick={this.toggleRegisterForm}
                    > I don't have an account? </button>
                </div>
            )
        }
        else{
            return (
                <div className={"login-card"}>
                    <RegisterForm onLogin={this.props.onLogin} />
                </div>
            )
        }
    }
}