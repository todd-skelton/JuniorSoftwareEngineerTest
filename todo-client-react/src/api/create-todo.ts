import axios from "axios";

export type CreateTodoRequest = {
    text: string;
}

export const createTodo = async (request: CreateTodoRequest) =>
    await axios.post("api/todos", request);