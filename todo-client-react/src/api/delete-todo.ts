import axios from "axios";

export type DeleteTodoRequest = {
    todoId: string;
}

export const deleteTodo = async (request: DeleteTodoRequest) =>
    await axios.delete(`api/todos/${request.todoId}`);