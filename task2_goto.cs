var fullText = new string[maxWords];
var text_checker = new int[maxWords , repeatLimit];
var curr_char = 0;
var page_indet = 1;
var count_line = 0;
var lineIndet = "";
const int p_size = 45, repeatLimit = 100, maxWords = 100000;
var text_reader = new StreamReader("TheText_insert.txt");

start:
	lineIndet = text_reader.ReadLine();
	if(lineIndet == "")
		goto split_func_cl;
	else 
		count_line++;
	
	if(count_line % p_size == 0)
	{
		page_indet++;
	}

	var endWord_index = 0;
	var char_index = 0;

	split_func_op:
		if(lineIndet is null)
			goto split_func_cl;
		
		if(char_index == lineIndet?.Length)
		{
			var inn_char = 0;
			var curWord = lineIndet[endWord_index..char_index];
			if(curWord.Length < 5)
				goto start;

			dup_word_check:
				if(inn_char == curr_char)
					goto page_insert_num;
		
				if(fullText[inn_char] == curWord)	
					goto next_page;

				inn_char++;
				goto dup_word_check;
	
			next_page:
				var inn_page_char = 0;
				
				next_page_loop:
					if(inn_page_char == repeatLimit - 1)
						goto start;
					if(text_checker[inn_char,inn_page_char] == page_indet)
						goto start;
					if(text_checker[inn_char,inn_page_char] == 0)
					{
						text_checker[inn_char,inn_page_char] = page_indet;
						goto start;
					}
					inn_page_char++;
					goto next_page_loop;
				
			page_insert_num:
				fullText[curr_char] = curWord;
				var page_insert_num_ind = 0;
				
				page_insert_num_loop:
					if(text_checker[curr_char, page_insert_num_ind] == 0)
					{
						text_checker[curr_char, page_insert_num_ind] = page_indet;
						goto new_line;
					}
					if(text_checker[inn_char,page_insert_num_ind] == page_indet) 
					{ 
						goto new_line;
					}
					page_insert_num_ind++;
					goto page_insert_num_loop;

			new_line:
			curr_char++;
			goto start;
		}	
		else
		if (((int)lineIndet[char_index]) == ' ') 
		{
			var inn_char = 0;
			var curWord = lineIndet[endWord_index..char_index];
			if(curWord.Length < 5)
				goto next_word_dup_checker;

			dup_word_check:
				if(inn_char == curr_char)
					goto page_insert_num;
		
				if(fullText[inn_char] == curWord)	
					goto next_page;

				inn_char++;
				goto dup_word_check;
	
			next_page:
				var inn_page_char = 0;
				
				next_page_loop:
					if(inn_page_char == repeatLimit - 1)
						goto next_word_dup_checker;
					if(text_checker[inn_char,inn_page_char] == page_indet)
						goto next_word_dup_checker;
					if(text_checker[inn_char,inn_page_char] == 0)
					{
						text_checker[inn_char,inn_page_char] = page_indet;
						goto next_word_dup_checker;
					}
					inn_page_char++;
					goto next_page_loop;
				
			page_insert_num:
				fullText[curr_char] = curWord;
				var page_insert_num_ind = 0;
				
				page_insert_num_loop:
					if(text_checker[curr_char, page_insert_num_ind] == 0)
					{
						text_checker[curr_char, page_insert_num_ind] = page_indet;
						goto nextWord;
					}
					if(text_checker[inn_char,page_insert_num_ind] == page_indet)
					{
						goto nextWord;
					}
					page_insert_num_ind++;
					goto page_insert_num_loop;

			nextWord:
			endWord_index = ++char_index;
			curr_char++;
			goto split_func_op;

			next_word_dup_checker:
			endWord_index = ++char_index;
			goto split_func_op;
		}
		else
		if ((int)lineIndet[char_index] >= 0x41 && (int)lineIndet[char_index] < 0x5A)
		{
			var oldChar_UTF16LE = (int)lineIndet[char_index];
			var newChar_UTF16LE =  oldChar_UTF16LE + 0x20;
			var newChar = $"{(char)newChar_UTF16LE}";
			
			lineIndet = lineIndet[0..char_index] + newChar + lineIndet[(char_index+1)..lineIndet.Length];
			char_index++;
			goto split_func_op;
		}
		else
		if ((int)lineIndet[char_index] < 0x40)
		{
			lineIndet = lineIndet[0..char_index] + lineIndet[(char_index+1)..lineIndet.Length];
			goto split_func_op;
		}
		char_index++;
		goto split_func_op;

	split_func_cl:
		int out_counter = 0;
		var a = new string[curr_char];
		var c = text_checker;
		var b = new int[a.Length];

	int counter = 0;
	loop_fill:
		b[counter] = counter;
		a[counter] = fullText[counter];
		if(counter++ != b.Length - 1)
		goto loop_fill;

	out_lp:
	int inn_counter = out_counter + 1; 
	if(inn_counter == a.Length)
		goto sort_cl;

	inn_lp:
		bool comp_rez = false;
		string aS = a[out_counter];
		string bS = a[inn_counter];

		var n;
		if (aS.Length < bS.Length)
		{
			n = aS.Length;
		}
		else
		{
			n = bS.Length;
		}

		int comp_counter = 0;
		comp_lp:
		if(aS[comp_counter] > bS[comp_counter])
		{
			comp_rez = true;
			goto compare_cl;
		}
		if(aS[comp_counter] < bS[comp_counter])
		{
			comp_rez = false;
			goto compare_cl;
		}
		if(comp_counter++ < n - 1)
		goto comp_lp;
		comp_rez = false;
		
		compare_cl:

		if(comp_rez)
		{
			string temp = "";
			int tempIndex = 0;
	
			temp = a[out_counter];
			a[out_counter] = a[inn_counter];
			a[inn_counter] = temp;

			tempIndex = b[out_counter];
			b[out_counter] = b[inn_counter];
			b[inn_counter] = tempIndex;
		}
		if(inn_counter++ < a.Length - 1)	
			goto inn_lp;
	
	if(out_counter++ < a.Length - 1)
	goto out_lp;

sort_cl:
	var fin_word_count = 0;
	var output = new string[curr_char];
	var outter_string = "";
	var curr_char_concat = 0;
	
	concat_loop:
		outter_string = "";
		if(curr_char_concat == curr_char - 1)
			goto end;
		outter_string += a[curr_char_concat] + " - ";

		int new_word_ind = curr_char_concat,
			count_page = 0;
		var trans_list_index = b[new_word_ind];
		
		concat_inn_lp:
			if (text_checker[trans_list_index, count_page] != 0)
			{
				outter_string += text_checker[trans_list_index, count_page] + " ";
				count_page++;
				goto concat_inn_lp;
			}
					
			output[fin_word_count++] = $"{outter_string}";
			curr_char_concat++;
			goto concat_loop;

end:
	File.WriteAllLines("TheText_quit.txt", output);
	text_reader.Close();
