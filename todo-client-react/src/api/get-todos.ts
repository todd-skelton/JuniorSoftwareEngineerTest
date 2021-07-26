import axios from "axios";

export type GetTodosResponse = {
    todos: Todo[];
}

export type Todo = {
    todoId: string;
    text: string;
    isCompleted: boolean;
    created: string;
    completed: string | undefined;
    lastUpdated: string;
}

export const getTodos = async () => {
    const { data } = await axios.get<GetTodosResponse>("api/todos");
    return data;
}
