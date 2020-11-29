const arr = [
  [9, 9, 7],
  [9, 7, 2],
  [6, 9, 5],
  [9, 1, 2]
];
function solution(A) {
  const n = A.length,
    m = A[0].length;
  let h = 0,
    nums = [];
  const all = [];
  const count = numberOfPaths(n, m);

  go(A, 0, 0, n, m, nums, all, count);

  console.log(all);
}
function go(A, i, j, n, m, nums, all, count) {
  const jok = !!(m - 1 - j < 0.1);
  const iok = !!(n - 1 - i < 0.1);

  if (nums.length < m + n - 1) {
    nums.push(A[i][j]);
  } else {
    all.push(nums);
    nums = [];
  }

  if (jok) {
    for (let k = i + 1; k < n; ++k) {
      if (nums.length < m + n - 1) {
        nums.push(A[k][j]);
      } else {
        all.push(nums);
        nums = [];
      }
    }
    //console.log(all);
    return;
  }
  if (iok) {
    for (let k = j + 1; k < m; ++k) {
      if (nums.length < m + n - 1) {
        nums.push(A[i][k]);
      } else {
        all.push(nums);
        nums = [];
      }
    }
    //console.log(all);
    return;
  }

  if (nums.length < m + n - 1) {
    nums.push(A[i][j]);
  } else {
    all.push(nums);
    nums = [];
  }

  go(A, i + 1, j, n, m, nums, all, count);
  go(A, i, j + 1, n, m, nums, all, count);
}
solution(arr);

function numberOfPaths(m, n) {
  // We have to calculate m+n-2 C n-1 here
  // which will be (m+n-2)! / (n-1)! (m-1)!
  let path = 1;
  for (let i = n; i < m + n - 1; ++i) {
    path *= i;
    path /= i - n + 1;
  }
  return path;
}
