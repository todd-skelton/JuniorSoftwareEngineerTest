import axios from "axios";

export type SetTodoIsCompletedRequest = {
    todoId: string;
    isCompleted: boolean;
}

export const setTodoIsCompleted = async (request: SetTodoIsCompletedRequest) =>
    await axios.post(`api/todos/${request.todoId}/is-completed`, { isCompleted: request.isCompleted });