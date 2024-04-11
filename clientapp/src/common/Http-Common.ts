import axios from "axios";

export default axios.create({
    baseURL: "http://localhost:44378/api",
    headers: {
        "Content-type": "application/json"
    }
});
