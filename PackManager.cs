using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService {
    public class PackManager {
        private static Random random = new Random();
        public static List<int> GetPack() {
            List<int> list = new List<int>();
            for (int i = 1; i <= 13; i++) {
                list.Add(i + 100);
                list.Add(i + 200);
                list.Add(i + 300);
                list.Add(i + 400);
            }
            int t, index;
            for (int i = 0, imax = list.Count; i < imax; i++) {
                index = random.Next(imax);
                t = list[index];
                list[index] = list[i];
                list[i] = t;
            }
            return list;
        }


        private static List<int> arr = new List<int>();
        public static int GetPackVal(List<int> list) {//计算大小  < 10000 
            //if (list.Count < 5) {
            //    return 0;
            //}
            arr.Clear();
            int sum = 0;
            int _t;
            for (int i = 0; i < list.Count; i++) {
                _t = Math.Min(10, list[i] % 100);
                if (_t < 10) {
                    sum += _t;
                    arr.Add(_t);
                }
            }
            if (arr.Count < 3) {
                while (sum <= 0) {
                    sum += 10;
                }
                while (sum > 10) {
                    sum -= 10;
                }
                return sum;
            } else if (arr.Count == 3) {
                _t= sum % 10;
                if (arr.Contains(_t)) {
                    return _t;
                } else {
                    if (_t == 0) {
                        return 10;
                    } else {
                        return 0;
                    }
                }
            } else if (arr.Count == 4) {
                _t= sum % 10;
                if (arr.Contains(_t)) {
                    return _t;
                } else {
                    for (int i = 0; i < arr.Count; i++) {
                        _t = arr[0];
                        arr.RemoveAt(0);
                        if (arr.Contains((sum - _t) % 10)) {
                            _t = sum % 10;
                            if (_t == 0) {
                                return 10;
                            } else {
                                return _t;
                            }
                        } else {
                            arr.Add(_t);
                        }
                    }
                }
            } else {//5
                for (int i = 0; i < arr.Count; i++) {
                    _t = arr[0];
                    arr.RemoveAt(0);
                    if (arr.Contains((sum - _t) % 10)) {
                        _t = sum % 10;
                        if (_t == 0) {
                            return 10;
                        } else {
                            return _t;
                        }
                    } else {
                        arr.Add(_t);
                    }
                }
            }
            return 0;
        }
    }
}
