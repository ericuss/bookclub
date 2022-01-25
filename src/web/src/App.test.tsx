import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders books link', () => {
  render(<App />);
  const linkElement = screen.getByText(/Books/i);
  expect(linkElement).toBeInTheDocument();
});
