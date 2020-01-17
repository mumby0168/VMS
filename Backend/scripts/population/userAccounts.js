const fs = require('fs');
const axios = require('axios');
var atob = require('atob');

var data = fs.readFileSync("users.json");
users = JSON.parse(data);

const identityUrl = "http://localhost:5010"
const gatewayUrl = "http://localhost:5020"
const createUserUrl = `${identityUrl}/api/users/create`
const signInUrl = `${identityUrl}/api/users/sign-in`
const completeUserUrl = `${identityUrl}/api/users/complete`
const getCodesUrl = `${identityUrl}/api/dev/accountCodes`
const createUserDetailsUrl = `${gatewayUrl}/gateway/api/users/create`
const signInUserUrl = `${gatewayUrl}/gateway/api/users/in`
const signoutUserUrl = `${gatewayUrl}/gateway/api/users/out`
const userInfoUrl = `${gatewayUrl}/gateway/api/users/info`

const adminUser = {
    email: "b-admin@test.com",
    password: "Test123"
}

const processJwt = (jwt) => {
    var body = jwt.toString().split('.')[1];
    body = atob(body);
    body = JSON.parse(body);
    return {
        id: body.nameid,
        email: body.email,
        businessId: body.businessId,
        role: body.role
    }
}


//SETUP
async function runAccountProcess() {

    //STEP 1: LOGIN ADMIN

    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(1);

    var jwt = null;
    var res = await axios.post(signInUrl, adminUser)
    jwt = res.data.jwt;

    if(jwt === null || undefined) {
        console.log("jwt not received");
        return;
    }    

    var token =  processJwt(jwt);

    //STEP 2: Create Accounts

    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(2);
    
    for(var i = 0; i < users.length; i++) {

        const user = users[i];
        const index = i;

        var res = await axios.post(createUserUrl, {
            email: user.email
        }, 
        {
        headers: {
            'Authorization': `Bearer ${jwt}`
        }});

        if(res === null || undefined) {
            console.log("Response was null for creating account: " + user.email);                       
            return;
        }

        if(res.status === 200) {
            console.log(`CREATE: user: ${index} reponse: ${res.status}`);
        }
        else {
            console.log("Creating user failed");
            return;
        }
        
    }

    


    //STEP 3: Get Codes

    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(3);

    var res = await axios.get(getCodesUrl);
    var codes = res.data;

    console.log(codes.length);

    //STEP 4: Complete Accounts

    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(4);

    for(i = 0; i < users.length; i++) {
        
        const index = i;
        const user = users[i];
                
        var code = codes.find(c => c.email === user.email);

        if(code === undefined || null) {
            console.log(`user: ${index} could not match code.`);
            return;
        }

        console.log(code);
        console.log(user);

        const toSend = {
            code: code.code,
            email: user.email,
            password: user.password,
            passwordConfirmation: user.passwordConfirm
        }

        console.log(toSend);
        
        var res = await axios.post(completeUserUrl, toSend);        

        if(res === null || undefined) {
            console.log("Response was null for completing account: " + user.email);            
            return;         
        }

        if(res.status === 200) {
            console.log(`${index} completed`);
        }

        console.log(`COMPLETE: user: ${index} reponse: ${res.status}`);
    };

};


async function createUserDetails() {
    //STEP 5: Create User INFO
    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(5);


    for(var i = 0; i < users.length; i++) {

        var user = users[i];

        //SIGN IN
        var jwt = null;
        var res = await axios.post(signInUrl, {
            email: user.email,
            password: user.password
        })

        //GET JWT & PROCESS
        jwt = res.data.jwt;

        if(jwt === null || undefined) {
            console.log("jwt not received");
            return;
        }    

        var token =  processJwt(jwt);       
        
        //MAKE DETAILS REQUEST

        const dataToPost = {
            accountId: token.id,
            businessId: token.businessId,
            basedSiteId: "aef48a96-1db9-488f-9cbf-034aab882f4b",
            firstName: user.firstName,
            secondName: user.secondName,
            phoneNumber: user.phoneNumber,
            businessPhoneNumber: user.phoneNumber
        }
        
        var res = await axios.post(createUserDetailsUrl, dataToPost, {headers: {'Authorization': `Bearer ${jwt}`}});

        if(res === null || undefined) {
            console.log(i + " failed to complete user details");
            break;
        }

        console.log("User Details Completed");

    }
}


async function signInOutUsers() {
    //STEP 5: Create User INFO
    console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    console.log(5);

    var inOut = true;


    for(var i = 0; i < users.length; i++) {
        
        console.log("___________________________________________________________________________________________________________________")
        console.log(i + 1);
        
        try {
            
        var user = users[i];
        
        var user = users[i];

        //SIGN IN    
    
        var jwt = null;
        console.log(1)
        console.log(user);
        var res = await axios.post(signInUrl, {
            email: user.email,
            password: user.password
        })        
        console.log(1.1);
        console.log(user);
        console.log(res.data);

        //GET JWT & PROCESS        
      
        jwt = res.data.jwt;

        if(jwt === null || undefined) {
            
            return;
        }    

        var token =  processJwt(jwt);       

        
        console.log(2);
        var info = await axios.get(userInfoUrl,  {headers: {'Authorization': `Bearer ${jwt}`}});
        console.log(2.1);
        console.log(info.data);

        if(info === null || undefined) {
            
            break;
        }

        var userInfo = info.data;                
        

        //Sign in or out users.
        if(inOut) {
            var res = await axios.post(signInUserUrl, {userid: userInfo.id}, {headers: {'Authorization': `Bearer ${jwt}`}});
            console.log(3);
            console.log(res.data);
        }
        else {
            var res = await axios.post(signoutUserUrl, {userid: userInfo.id}, {headers: {'Authorization': `Bearer ${jwt}`}});
            console.log(3);
            console.log(res.data);
        }        
        inOut = !inOut                        
            
        } catch (error) {
            console.log(error);
        }
        
    }
}


signInOutUsers();
