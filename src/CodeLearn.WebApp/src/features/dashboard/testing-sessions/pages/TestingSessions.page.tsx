import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { TypographyH2 } from '@/components/typography/typography-h2.tsx';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select.tsx';
import Editor from '@monaco-editor/react';
import { Badge } from '@/components/ui/badge.tsx';

function TestingSessionsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Сессия тестирования');
  }, []);

  return (
    <div className="space-y-2">
      <TypographyH2>Продвинутый тест знаний языка C#</TypographyH2>
      <div className="p-1"></div>
      <h2 className="text-lg font-bold text-zinc-900">ПИБ-01, Мышкин В.Ю.</h2>
      <div className="my-2 text-zinc-600">27.05.24, 20:24 - 21:54 (90 мин.)</div>
      <div className="space-y-0.5">
        <div className="text-zinc-800">Задачи на программирование: 1 из 1</div>
        <div className="text-zinc-800">Ответы на вопросы: 1 из 1</div>
        <div className="text-zinc-800">Решено: 2/2, 100%</div>
      </div>

      <div className="p-0.5"></div>
      <div className="font-semibold text-zinc-900">Задачи</div>
      <Card className="">
        <CardHeader>
          <CardTitle className="-mt-1">Перевод из римского в целое</CardTitle>
          <CardDescription>Римские цифры представлены семью различными символами: I, ...</CardDescription>
        </CardHeader>
        <CardContent>
          <form>
            <div className="grid w-full items-center gap-4">
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="name">Решение</Label>
                <Select>
                  <SelectTrigger id="framework">
                    <SelectValue placeholder="Решение 5 - 27.05.24, 20:27" />
                  </SelectTrigger>
                </Select>
              </div>
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="framework">Код решения</Label>
                <div className="h-40 rounded-lg border border-zinc-200 p-0.5">
                  <Editor
                    defaultLanguage="csharp"
                    options={{
                      minimap: {
                        enabled: false,
                      },
                      suggest: {
                        showWords: true,
                        showClasses: true,
                      },
                      padding: {
                        top: 6,
                      },
                      wordWrap: 'on',
                      fontSize: 14,
                    }}
                  />
                </div>
              </div>
              <Badge className="w-fit bg-green-200 text-green-800">Решение верное</Badge>
            </div>
          </form>
        </CardContent>
      </Card>
      <Card className="">
        <CardHeader>
          <CardTitle className="-mt-1">Алгоритм 1</CardTitle>
          <CardDescription>Найдите площадь.</CardDescription>
        </CardHeader>
        <CardContent>
          <form>
            <div className="grid w-full items-center gap-4">
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="name">Решение</Label>
                <Select>
                  <SelectTrigger id="framework">
                    <SelectValue placeholder="Решение 1 - 27.05.24, 20:49" />
                  </SelectTrigger>
                </Select>
              </div>
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="framework">Код решения</Label>
                <div className="h-40 rounded-lg border border-zinc-200 p-0.5">
                  <Editor
                    defaultLanguage="csharp"
                    options={{
                      minimap: {
                        enabled: false,
                      },
                      suggest: {
                        showWords: true,
                        showClasses: true,
                      },
                      padding: {
                        top: 6,
                      },
                      wordWrap: 'on',
                      fontSize: 14,
                    }}
                  />
                </div>
              </div>
              <Badge className="w-fit bg-green-200 text-green-800">Решение верное</Badge>
            </div>
          </form>
        </CardContent>
      </Card>
      <Card className="">
        <CardHeader>
          <CardTitle className="-mt-1">Алгоритм 1</CardTitle>
          <CardDescription>Найдите площадь.</CardDescription>
        </CardHeader>
        <CardContent>
          <form>
            <div className="grid w-full items-center gap-4">
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="name">Решение</Label>
                <Select>
                  <SelectTrigger id="framework">
                    <SelectValue placeholder="Решение 1 - 27.05.24, 20:49" />
                  </SelectTrigger>
                </Select>
              </div>
              <div className="flex flex-col space-y-1.5">
                <Label htmlFor="framework">Код решения</Label>
                <div className="h-40 rounded-lg border border-zinc-200 p-0.5">
                  <Editor
                    defaultLanguage="csharp"
                    options={{
                      minimap: {
                        enabled: false,
                      },
                      suggest: {
                        showWords: true,
                        showClasses: true,
                      },
                      padding: {
                        top: 6,
                      },
                      wordWrap: 'on',
                      fontSize: 14,
                    }}
                  />
                </div>
              </div>
              <Badge className="w-fit bg-green-200 text-green-800">Решение верное</Badge>
            </div>
          </form>
        </CardContent>
      </Card>
      <div className="p-0.5"></div>
      <div className="font-semibold text-zinc-900 ">Вопросы</div>
      <div></div>
    </div>
  );
}

export default TestingSessionsPage;
