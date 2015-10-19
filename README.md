# simple-linq [![Build Status](https://travis-ci.org/nbarbettini/simple-linq.svg?branch=master)](https://travis-ci.org/nbarbettini/simple-linq.svg)

If you are trying to build a custom LINQ provider, you may notice that it's [not](http://ayende.com/blog/62465/the-pain-of-implementing-linq-providers) [easy](http://stackoverflow.com/a/714232/3191599).
[Remotion re-linq](https://relinq.codeplex.com/) is a great framework for reducing the friction of writing a full LINQ provider from scratch. If you're trying to build a *very* basic provider, though, even re-linq might be overkill.

You can think of it this way, where the evaluation is the complexity of your provider: [simple-linq](https://github.com/nbarbettini/simple-linq) < [Relinq](https://github.com/re-motion/Relinq) < LINQ from scratch

simple-linq provides an easy starting point for evaluating simple queries (no projections, no aggregations).
