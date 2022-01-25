import { BrowserRouter as Router, Route } from 'react-router-dom';
import { ReactComponent as Logo } from './assets/images/logo.svg';
import { BookList } from './app/books/book-list';
import './App.css';
import "./index.css"

function App() {
  return (
    <div className="App">
      <Router>
        <header className="App-header">
          <main>
            <nav>
              <ul className="App-menu">
                <Logo className="App-menu-item--logo"></Logo>
                <li className="App-menu-item"><a href="/Books">Books</a></li>
              </ul>
            </nav>
          </main>
        </header>
        <div className="App-content">
          <Route path="/" exact component={BookList} />
          <Route path="/Books" exact component={BookList} />
        </div>
      </Router>

    </div>
  );
}

export default App;

