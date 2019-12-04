export default function reducer(state = { fetching: false, error: null, logs:[]}, action) {    

  switch (action.type) {             

      case "FETCH_LOGS": {      
        console.log(action.type);
          return {...state, fetching: true};
      }

      case "LOGS_FETCHED": {
        console.log(action.type);
          return {...state, fetching: false, logs: action.payload};
      }

      case "FETCH_LOGS_REJECTED": {
        console.log(action.type);
          return {...state, fetching: false, error: action.payload};
      }

      case "PURGE_LOGS": {
        console.log(action.type);
          return {...state, logs: []};
      }
      
      default:
        return state
  }

}