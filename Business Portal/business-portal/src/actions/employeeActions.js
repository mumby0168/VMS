export function openEmployeeRecords(employee) {
    return {
        type: "OPEN_EMPLOYEE_RECORDS",
        payload: employee
    }
}


export function closeEmployeeRecords(employee) {
    return {type: "CLOSE_EMPLOYEE_RECORDS"};
}