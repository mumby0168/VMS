export const isEmailValid = (email: string): boolean => {
    return email.includes('@');
}

export const isPostCodeValid = (postcode: string): boolean => {
    //TODO: add some regex validation here.
    return true;
}

