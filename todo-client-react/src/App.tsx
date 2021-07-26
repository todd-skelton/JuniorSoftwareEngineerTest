import { AppBar, Box, Checkbox, IconButton, List, ListItem, ListItemIcon, ListItemSecondaryAction, ListItemText, ListSubheader, Tab, Tabs, TextField, Toolbar, Typography } from '@material-ui/core';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Delete } from '@material-ui/icons';
import React, { Fragment, useEffect, useState } from 'react';
import { createTodo } from './api/create-todo';
import { deleteTodo } from './api/delete-todo';
import { getTodos, Todo } from './api/get-todos';
import { setTodoIsCompleted } from './api/set-todo-is-completed';

const App = () => {
  const [tab, setTab] = useState(0);
  const [text, setText] = useState("");
  const [todos, setTodos] = useState<Todo[]>([]);

  useEffect(() => {
    updateTodos();
  }, []);

  const updateTodos = async () => {
    const response = await getTodos();
    setTodos(response.todos);
  }

  const handleTabChange = (_: React.ChangeEvent<{}>, newValue: number) => {
    setTab(newValue);
  };

  const handleTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setText(event.target.value);
  }

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    await createTodo({ text });
    setText("");
    await updateTodos();
  }

  const handleSetIsCompleted = (todoId: string, isCompleted: boolean) => async () => {
    await setTodoIsCompleted({ todoId, isCompleted });
    await updateTodos();
  }

  const handleDeleteTodo = (todoId: string) => async () => {
    await deleteTodo({ todoId });
    await updateTodos();
  }

  return (
    <Fragment>
      <CssBaseline />
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6">Todos</Typography>
        </Toolbar>
      </AppBar>
      <Box m={2}>
        <form onSubmit={handleSubmit}>
          <TextField label="What do you need to do?" variant="outlined" fullWidth value={text} onChange={handleTextChange} />
        </form>
        <Tabs
          value={tab}
          onChange={handleTabChange}
          indicatorColor="primary"
          textColor="primary"
        >
          <Tab label="Todo List" />
          <Tab label="Audit Log" />
        </Tabs>
        <div role="tabpanel" hidden={tab !== 0}>
          <List>
            {todos.filter(e => !e.isCompleted).map(todo => (
              <ListItem key={todo.todoId} button onClick={handleSetIsCompleted(todo.todoId, true)}>
                <ListItemIcon>
                  <Checkbox
                    edge="start"
                    checked={todo.isCompleted}
                    tabIndex={-1}
                    disableRipple
                  />
                </ListItemIcon>
                <ListItemText primary={todo.text} />
                <ListItemSecondaryAction>
                  <IconButton edge="end" onClick={handleDeleteTodo(todo.todoId)}>
                    <Delete />
                  </IconButton>
                </ListItemSecondaryAction>
              </ListItem>
            ))}
            {todos.some(e => e.isCompleted) && <ListSubheader>Completed</ListSubheader>}
            {todos.filter(e => e.isCompleted).map(todo => (
              <ListItem key={todo.todoId} button onClick={handleSetIsCompleted(todo.todoId, false)}>
                <ListItemIcon>
                  <Checkbox
                    edge="start"
                    checked={todo.isCompleted}
                    tabIndex={-1}
                    disableRipple
                  />
                </ListItemIcon>
                <ListItemText primary={todo.text} />
                <ListItemSecondaryAction>
                  <IconButton edge="end" onClick={handleDeleteTodo(todo.todoId)}>
                    <Delete />
                  </IconButton>
                </ListItemSecondaryAction>
              </ListItem>
            ))}
          </List>
        </div>
      </Box>
    </Fragment>
  );
}

export default App;