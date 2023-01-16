import React from "react";

type BrandFormState = {
    brandName: string
}


export class BrandForm extends React.Component<void, any>{

    state: BrandFormState = {
        brandName: ""
    }

    render() {
        return (
            <form className={"brand-form"}>
                <label>
                    Enter new brand:
                </label>
            </form>
        )
    }


}