import { BrowserRouter as Router, Route } from 'react-router-dom';
import { ReactComponent as Logo } from './assets/images/logo.svg';
import { BookList } from './app/books/book-list';
import { Vote } from './app/vote/vote';
import { CreateVoteList } from './app/create-vote-list/create-vote-list';
import { VoteListDetail } from './app/vote-list-detail/vote-list-detail';
import { SignIn } from './app/sign-in/sign-in';
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
                <li className="App-menu-item"><a href="/public/Books">Books</a></li>
                <li className="App-menu-item"><a href="/public/CreateVoteList">Create Vote List</a></li>
                <li className="App-menu-item"><a href="/public/Vote">Vote</a></li>
              </ul>
            </nav>
          </main>
        </header>
        <div className="App-content">
          <Route path="/" exact component={BookList} />
          <Route path="/public/" exact component={BookList} />
          <Route path="/public/Books" exact component={BookList} />
          <Route path="/public/CreateVoteList" exact component={CreateVoteList} />
          <Route path="/public/Vote" exact component={Vote} />
          <Route path="/public/vote/:id" component={VoteListDetail} />
          <Route path="/public/sign-in" exact component={SignIn} />
        </div>
      </Router>

    </div>
  );
}

export default App;

