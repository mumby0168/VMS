import React from 'react'
import TextFieldWrapper from '../../common/TextFieldWrapper'
import { Button, makeStyles, Select, MenuItem, FormControl, InputLabel } from '@material-ui/core';

const phoneRegex = /^\d+$/;

const useStyles = makeStyles(theme => ({
    seperation: {
        margin: theme.spacing(1)
    }
}))

export default function CompleteUserForm(props) {

    const { errors, data, handleChange, handleBlur, sites, handleSiteChanged, handleFormSubmit } = props;

    const classes = useStyles();


    const siteOptions = sites.map((site, index) => {
        return <MenuItem key={index} value={site.id}>{site.name}</MenuItem>
    })

    const validateFirstName = () => {
        return errors.firstName ?
            data.firstName.length === 0 :
            false;
    }

    const validateSecondName = () => {
        return errors.secondName ?
            validPhoneNumber(data.phone) :
            false;
    }

    const validatePhone = () => {
        return errors.phone ?
            data.phone.length === 0 : false
    }

    const validateBusinessPhone = () => {
        return errors.businessPhone ?
            validPhoneNumber(data.businessPhone) : false
    }

    const validPhoneNumber = (number) => {
        return !phoneRegex.test(number);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(data);
        console.log(validateFirstName());
        console.log(validateSecondName());
        console.log(validateBusinessPhone());
        console.log(validatePhone());
        if (validateFirstName() || validatePhone() || validateSecondName() || validateBusinessPhone() || data.selectedSiteId === "") {
            console.log("invalid form!");
        }
        else {
            handleFormSubmit();
            return;            
        }
    }


    return (
        <form>
            <TextFieldWrapper
                className={classes.seperation}
                name="firstName"
                onBlur={(e) => handleBlur(e.target.name)}
                fullWidth
                error={validateFirstName()}
                value={data.firstName}
                label="First Name"
                onChange={(e) => handleChange(e.target.value, "firstName")}
                helperText={validateFirstName() ? "Please enter your first name" : ""}
                placeholder="Jeff"
            />
            <TextFieldWrapper
                className={classes.seperation}
                name="secondName"
                onBlur={(e) => handleBlur(e.target.name)}
                fullWidth
                error={validateSecondName()}
                value={data.secondName}
                label="Second Name"
                onChange={(e) => handleChange(e.target.value, e.target.name)}
                helperText={validateSecondName() ? "Please enter your second name" : ""}
                placeholder="Bloggs"
            />
            <TextFieldWrapper
                className={classes.seperation}
                name="phone"
                onBlur={(e) => handleBlur(e.target.name)}
                fullWidth
                error={validatePhone()}
                value={data.phone}
                label="Personal Phone"
                onChange={(e) => handleChange(e.target.value, e.target.name)}
                helperText={validatePhone() ? "Please enter a vaild phone number" : ""}
                placeholder="07568 465345"
            />
            <TextFieldWrapper
                className={classes.seperation}
                name="businessPhone"
                onBlur={(e) => handleBlur(e.target.name)}
                fullWidth
                error={validateBusinessPhone()}
                value={data.businessPhone}
                label="Business Phone"
                onChange={(e) => handleChange(e.target.value, e.target.name)}
                helperText={validateBusinessPhone() ? "Please enter a vaild phone number" : ""}
                placeholder="01242 342345"
            />
            <FormControl fullWidth className={classes.seperation}>
                <InputLabel id="selected-site">Based Site</InputLabel>
                <Select value={data.selectedSiteId} onChange={(e) => handleSiteChanged(e.target.value)} labelId="selected-site">
                    {siteOptions}
                </Select>
            </FormControl>


            <Button onClick={handleSubmit} className={classes.seperation} variant="contained" color="secondary" type="submit">Complete</Button>
        </form>
    )
}
