string text = File.ReadAllText(@"TheText.txt");
            int text_length = text.Length;
            int i = 0;
            int Max_Out = 5;
            string current_word = "";
            string[] text_arr = new string[1000];
            int word_count = 0;
            int k = 0;
        while_loop:
            if ((text[i] >= 65) && (text[i] <= 90) || (text[i] >= 97) && (text[i] <= 122) || text[i] == 45)
            {
                if ((text[i] >= 65) && (text[i] <= 90))
                {
                    current_word += (char)(text[i] + 32);
                }
                else
                {
                    current_word += text[i];
                }
            }
            else
            {
                if (current_word != "" && current_word != null && current_word != "-" && current_word != "no"  && current_word != "the" && current_word != "by" && current_word != "and"  && current_word != "in" && current_word != "or" && current_word != "any" && current_word != "for" && current_word != "to" && current_word != "\"" && current_word != "a" && current_word != "on" && current_word != "of" && current_word != "at" && current_word != "is" && current_word != "\n" && current_word != "\r"  )
                {
                    text_arr[word_count] = current_word;
                    word_count++;
                }
                current_word = "";
            }
            i++;
            if (i < text_length)
            {
                goto while_loop;
            }
            else
            {
                if (current_word != "" && current_word != null && current_word != "-" && current_word != "no"  && current_word != "the" && current_word != "by" && current_word != "and"  && current_word != "in" && current_word != "or" && current_word != "any" && current_word != "for" && current_word != "to" && current_word != "\"" && current_word != "a" && current_word != "on" && current_word != "of" && current_word != "at" && current_word != "is" && current_word != "\n" && current_word != "\r"  )
                {
                    text_arr[word_count] = current_word;
                    word_count++;
                }
            }
            string[] uniq_arr = new string[1000];
            int[] uniq_arr_count = new int[1000];

            int amount_of_words = text_arr.Length;
            i = 0;
            int insertPos = 0;
            int j = 0;
            int dubs = 0;
        while_loop_count:
            insertPos = 0;
            int current_length = uniq_arr.Length;
            j = 0;

        for_loop:
            if (j < current_length && uniq_arr[j] != null)
            {
                if (uniq_arr[j] == text_arr[i])
                {
                    insertPos = j;
                    goto end_for_loop;

                }
                j++;
                goto for_loop;
            }
        end_for_loop:
            if (insertPos == 0)
            {
                uniq_arr[i - dubs] = text_arr[i];
                uniq_arr_count[i - dubs] = 1;
            }
            else
            {
                uniq_arr_count[insertPos] += 1;
                dubs++;
            }
            i++;
            if (i < amount_of_words && text_arr[i] != null)
            {
                goto while_loop_count;
            }
            int length = uniq_arr_count.Length;
            j = 0;

            goto sort_loop;

        sort_loop:
            int index1 = -1;
            int index2 = 0;
        inn_loop:
            index1++;
            if (index1 == word_count)
                goto print_loop;
            else
            {
                goto inn_loop_2;
            }
        inn_loop_2:
            if (uniq_arr_count[index2] < uniq_arr_count[index2 + 1])
            {
                var t = uniq_arr_count[index2];
                uniq_arr_count[index2] = uniq_arr_count[index2 + 1];
                uniq_arr_count[index2 + 1] = t;
                var t_str = text_arr[index2];
                text_arr[index2] = text_arr[index2 + 1];
                text_arr[index2 + 1] = t_str;
            }
            if (index2 != word_count)
            {
                index2++;
                goto inn_loop_2;
            }
            else
            {
                index2 = 0;
                goto inn_loop;
            };

        print_loop:
            Console.WriteLine(text_arr[k] + " - " + uniq_arr_count[k]);
            k++;
            if (text_arr[k] != null && uniq_arr_count[k] != 0 && Max_Out != 0)
            {
                Max_Out--;
                goto print_loop;
            }